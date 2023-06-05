import React from 'react';
import { Link, Outlet } from 'react-router-dom';

export function Component() { return Workshop(); }

function Workshop() {
    return (<>
        <p>Let's get to work!</p>
        <Link to='types'>Types</Link>
        <Outlet />
    </>
    );
}
