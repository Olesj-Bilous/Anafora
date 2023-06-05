import React from 'react';
import logo from './logo.svg';
import { Outlet, Link } from 'react-router-dom';
import './root.css';

function Root() {
    return (
        <>
            <nav>
                <Link to={'workshop'}>Workshop</Link>
                <Link to={'storefront'}>Storefront</Link>
            </nav>
            <main>
                <Outlet />
            </main>
        </>
    );
}

export default Root;
