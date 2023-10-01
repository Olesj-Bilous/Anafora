import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { QueryClient } from '@tanstack/react-query';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import Storefront from './routes/storefront/storefront';
import buildRouteQuery from './utils/buildRouteQuery';
import { array, ObjectSchema } from 'yup'
import { propertySchema, typesSchema, valuesSchema } from './routes/schemas/property';

async function fetchRemoteData<T>(pathname: string, validate: (data: any) => T) {
  const url = new URL('api' + pathname, 'https://localhost:7166')
  const response = await fetch(url)
  return validate(await response.json())
}

function castRemoteArray<T extends object>(schema: ObjectSchema<T>): (value: any) => T[] {
  return value => array(schema).default([]).json().cast(value) as T[]
}

function castRemoteObject<T extends object>(schema: ObjectSchema<T>): (value: any) => T | null {
  return value => schema.nullable().default(null).json().cast as T | null
}

const queryClient = new QueryClient()
const queryBuilder = buildRouteQuery(
  queryClient
)

function allQueries<T>(validate: (data: any) => T[], model: string) {
  return queryBuilder.setQueryFn<[model: string]>(
    ({ queryKey: [model] }) => fetchRemoteData(`/${model}/all`, validate)
  ).deriveQueryKey(
    () => [model]
  )
}

function getQueries<T>(validate: (data: any) => T, model: string) {
  return queryBuilder.setQueryFn<[model: string, id: string]>(
    ({ queryKey: [model, id] }) => fetchRemoteData(`/${model}/get?id=${id}`, validate)
  ).deriveQueryKey(
    ({ params: { id } }) => [model, id ?? '']
  )
}

function relationQueries<T>(validate: (data: any) => T, model: string, relation: string) {
  return queryBuilder.setQueryFn<[model: string, relation: string, id: string]>(
    ({ queryKey: [model, relation, id] }) => fetchRemoteData(`/${model}/${relation}?id=${id}`, validate)
  ).deriveQueryKey(
    ({ params: { id } }) => [model, relation, id ?? '']
  )
}

const router = createBrowserRouter([{
  path: '/',
  element: <Root />,
  errorElement: <ErrorPage />,
  children: [{
    path: 'workshop',
    lazy: () => import('./routes/workshop/workshop'),
    children: [{
      path: 'types',
      loader: allQueries(castRemoteArray(typesSchema), 'type').loader,
      lazy: () => import('./routes/workshop/types')
    }, {
      path: 'properties',
      loader: allQueries(castRemoteArray(propertySchema), 'property').loader,
      lazy: () => import('./routes/workshop/properties'),
      children: [{
        path: 'property/:id',
        loader: relationQueries(castRemoteArray(valuesSchema), 'property', 'values').loader,
        children: [{
          path: 'values'
        }]
      }]
    }]
  }, {
    path: 'storefront',
    element: <Storefront />
  }]
}]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
