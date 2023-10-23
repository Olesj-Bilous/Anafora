import { typeProperties } from "../queries/queries";
import { Property } from "./Property";


export default function Type({ id, name }: Type) {
  const { data: properties, isLoading } = typeProperties.useQuery({ id })
  return <>
    <h3>{name}</h3>
    {isLoading ? 'loading' : properties && <ul>{properties.map(prop => <li><Property {...prop} /></li>)}</ul>}
  </>
}
