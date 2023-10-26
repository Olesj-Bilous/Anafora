import {updateProperty } from "../queries/mutations"
import { getProperty, propertyValues } from "../queries/queries"

export function Property({ id }: Property) {
  const { data, isLoading } = getProperty.useQuery({id})
  const { data: values, isLoading: valuesLoading } = propertyValues.useQuery({ id })

  const mutator = updateProperty.useMutation()
  return <>
    <h4>{isLoading ? 'loading' : <input type="text" value={data?.name} onChange={({ target: { value } }) =>
      mutator.mutate({ id, name: value })}
    />}</h4>
    <ul>{valuesLoading ? 'values loading' : values && values.map(({ value }) => <li>{value}</li>)}</ul>
  </>
}
