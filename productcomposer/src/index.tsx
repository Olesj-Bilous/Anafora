import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import Storefront from './routes/storefront/storefront';
import buildRouteQuery from './utils/buildRouteQuery';
import { array, ObjectSchema } from 'yup'
import { propertySchema, typeSchema, valueSchema } from './schemas/model';

async function fetchRemoteData<T>(pathname: string, validate: (data: any) => T): Promise<T> {
  const url = new URL('api' + pathname, 'https://localhost:7166')
  const response = await fetch(url)
  return validate(await response.json())
}

function castRemoteArray<T extends object>(schema: ObjectSchema<T>): (value: any) => T[] {
  return value => array(schema).default([]).json().cast(value) as T[]
}

function castRemoteObject<T extends object>(schema: ObjectSchema<T>): (value: any) => T | null {
  return value => schema.nullable().default(null).json().cast(value) as T | null
}

const queryClient = new QueryClient()
const queryBuilder = buildRouteQuery(
  queryClient
)

function allQueries<T>(validate: (data: any) => T[], model: string) {
  return queryBuilder.setQueryFn(
    ({ queryKey: [model] }: { queryKey: [model: string] }) => fetchRemoteData(`/${model}/all`, validate)
  ).deriveQueryKey(
    () => [model]
  )
}

function getQueries<T>(validate: (data: any) => T, model: string) {
  return queryBuilder.setQueryFn(
    ({ queryKey: [model, id] }: { queryKey: [model: string, id: string] }) => fetchRemoteData(`/${model}/get?id=${id}`, validate)
  ).deriveQueryKey(
    ({id}) => [model, id ?? '']
  )
}

function relationQueries<T>(validate: (data: any) => T, model: string, relation: string) {
  return queryBuilder.setQueryFn(
    ({ queryKey: [model, relation, id] }: { queryKey: [model: string, relation: string, id: string] }) => fetchRemoteData(`/${model}/${relation}?id=${id}`, validate)
  ).deriveQueryKey(
    ({id}) => [model, relation, id ?? '']
  )
}

export const allTypeQueries = allQueries(castRemoteArray(typeSchema), 'type')

export const typePropertyQueries = relationQueries(castRemoteArray(propertySchema), 'type', 'properties')
export const propertyValueQueries = relationQueries(castRemoteArray(valueSchema), 'property', 'values')

const router = createBrowserRouter([{
  path: '/',
  element: <Root />,
  errorElement: <ErrorPage />,
  children: [{
    path: 'workshop',
    lazy: () => import('./routes/workshop/workshop'),
    children: [{
      path: 'types',
      loader: allTypeQueries.loader,
      lazy: () => import('./routes/workshop/types')
    }, {
      path: 'properties',
      loader: allQueries(castRemoteArray(propertySchema), 'property').loader,
      lazy: () => import('./routes/workshop/properties'),
      children: [{
        path: ':id',
        loader: relationQueries(castRemoteArray(valueSchema), 'property', 'values').loader,
        lazy: () => import('./routes/workshop/property')
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
  <QueryClientProvider client={queryClient}>
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode></QueryClientProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
