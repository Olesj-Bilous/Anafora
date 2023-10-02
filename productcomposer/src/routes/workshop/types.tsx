import { useLoaderData } from 'react-router-dom';
import { allTypeQueries } from '../..';
import Type from '../../components/Type';

function Types() {
  const types = useLoaderData() as Type[]

  return (<ul>{
    types.length
      ? types.map(type =>
        <li><Type {...type} /></li>)
      : <li>No types yet.</li>
  }</ul>);
}

export { Types as Component }
