import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import Storefront from './routes/storefront/storefront';
import { allProperties, allTypes } from './queries/queries';
import { createMutor } from './utils/setQueryFn';

const queryClient = new QueryClient()

export const mutor = createMutor(queryClient)

const router = createBrowserRouter([{
  path: '/',
  element: <Root />,
  errorElement: <ErrorPage />,
  children: [{
    path: 'workshop',
    lazy: () => import('./routes/workshop'),
    children: [{
      path: 'types',
      loader: allTypes.loader(queryClient),
      lazy: () => import('./routes/workshop/types')
    }, {
      path: 'properties',
      loader: allProperties.loader(queryClient),
      lazy: () => import('./routes/workshop/properties')
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
