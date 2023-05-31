import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import reportWebVitals from './reportWebVitals';
import './index.css';
import Root from './routes/root';
import ErrorPage from './routes/error-page';
import Storefront from './routes/storefront/storefront';

const router = createBrowserRouter([{
    path: '/',
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [{
        path: 'workshop',
        lazy: () => import('./routes/workshop/workshop'),
        children: [{
            path: 'types',
            loader: ({ request, params }) => fetchRemoteData(request, params),
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
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

export default function fetchRemoteData(request: Request, params: any) {
    const url = new URL(request.url);
    url.port = '7166';
    url.pathname = '/api' + url.pathname;
    return fetch(url);
}
