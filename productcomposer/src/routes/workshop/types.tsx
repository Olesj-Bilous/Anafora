import { allTypes } from '../../queries/queries';
import Type from '../../components/Type';

function Types() {
  const types = allTypes.useLoaderData()

  return (<ul>{
    types.length
      ? types.map(type =>
        <li><Type {...type} /></li>)
      : <li>No types yet.</li>
  }</ul>);
}

export { Types as Component }
