import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
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
    }, {
      path: 'signin',
      lazy: () => import('./routes/workshop/signin')
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
    </StrictMode>
  </QueryClientProvider>
);
