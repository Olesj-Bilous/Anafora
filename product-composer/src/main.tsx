import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import { allProperties, allTypes } from './queries/queries';
import { createMutor } from './utils/setQueryFn';
import remote from './queries/remote';

const queryClient = new QueryClient()

export const mutor = createMutor(queryClient)

const router = createBrowserRouter([{
  path: '/',
  element: <Root />,
  errorElement: <ErrorPage />,
  children: [{
    path: 'products',
    lazy: () => import('./routes/products'),
    children: [{
      path: 'types',
      loader: allTypes.loader(queryClient),
      lazy: () => import('./routes/products/types')
    }, {
      path: 'properties',
      loader: allProperties.loader(queryClient),
      lazy: () => import('./routes/products/properties')
    }]
  }, {
    path: 'signin',
    lazy: () => import('./routes/account/signin'),
    async action({ request }) {
      const data = await request.formData()
      const email = data.get('email'), password = data.get('password')
      const response = await remote('/account/signin', 'POST', { email, password })
      return response.text()
    }
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
