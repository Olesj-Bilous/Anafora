import React from 'react';
import { isRouteErrorResponse, useRouteError } from "react-router-dom";

function ErrorPage() {
    const error = useRouteError();
    const msg = errorMessage(error);

    return (
        <div id="error-page">
            <h1>Oops!</h1>
            <p>Sorry, an unexpected error has occurred.</p>
            <p>
                <i>{msg}</i>
            </p>
        </div>
    );
}

function errorMessage(error: unknown): string {
    if (isRouteErrorResponse(error)) {
        return `${error.status} ${error.statusText}`;
    }
    if (error instanceof Error) {
        return error.message;
    }
    if (typeof error === 'string') {
        return error;
    }
    return 'Unknown error';
}

export default ErrorPage;
