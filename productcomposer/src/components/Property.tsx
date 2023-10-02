import { propertyValueQueries } from "..";


export function Property({ id, name }: Property) {
  const { data: values } = propertyValueQueries.useQuery({ id })
  return <>
    <h4>{name}</h4>
    <ul>{
      values && values.map(({ value }) => <li>{value}</li>)
    }</ul>
  </>
}
