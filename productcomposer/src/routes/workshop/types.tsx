import React from 'react';
import { useLoaderData } from 'react-router-dom';

export function Component() {
    return Types();
}

function Types() {
    const types = useLoaderData() as Array<{ name: string }>;

    return (<ul>{
        types.length
            ? types.map(i =>
                <li>{i.name}</li>)
            : <li>No types yet.</li>
    }</ul>);
}
