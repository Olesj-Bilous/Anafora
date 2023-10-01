import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import Storefront from './routes/storefront/storefront';
import { QueryClient } from '@tanstack/react-query';
import buildRouteQuery from './utils/buildRouteQuery';

function fetchRemoteData(urlString: string) {
  const url = new URL(urlString)
  url.port = '7166'
  url.pathname = '/api' + url.pathname;
  return fetch(url);
}

const queryClient = new QueryClient()
const queryBuilder = buildRouteQuery(
  queryClient
).deriveQueryKey(
  ({request}) => [request.url]
).setQueryFn(
  ({queryKey: [url]}) => fetchRemoteData(url)
)

const router = createBrowserRouter([{
  path: '/',
  element: <Root />,
  errorElement: <ErrorPage />,
  children: [{
    path: 'workshop',
    lazy: () => import('./routes/workshop/workshop'),
    children: [{
      path: 'types',
      loader: queryBuilder.loader,
      lazy: () => import('./routes/workshop/types')
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
