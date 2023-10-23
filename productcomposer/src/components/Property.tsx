import { updateProperty } from "../queries/mutations"
import { propertyValues } from "../queries/queries"


export function Property({ id, name }: Property) {
  const { data: values, isLoading } = propertyValues.useQuery({ id })
  const mutator = updateProperty.useMutation()
  return <>
    <h4><input type="text" value={name} onChange={({target:{value}})=>mutator.mutate({id, name: value})} /></h4>
    <ul>{isLoading ? 'loading' : values && values.map(({ value }) => <li>{value}</li>)}</ul>
  </>
}
