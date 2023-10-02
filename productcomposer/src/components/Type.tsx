import { typePropertyQueries } from "..";
import { Property } from "./Property";


export default function Type({ id, name }: Type) {
  const {data:properties, isLoading} = typePropertyQueries.useQuery({ id })
  return <>
    <h3>{name}</h3>
    {!isLoading && properties && properties.map((prop => <Property {...prop} />))}
  </>
}
