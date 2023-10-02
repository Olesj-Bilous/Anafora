import { useLoaderData } from 'react-router-dom';

function Property() {
  const values = useLoaderData() as Array<{ value: string }>;

  return (<ul>{
    values.length
      ? values.map(i =>
        <li>{i.value}</li>)
      : <li>No values yet.</li>
  }</ul>);
}

export { Property as Component }
