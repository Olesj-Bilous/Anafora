import { allProperties } from "../../queries/queries"
import { Property } from "../../components/Property"

function Properties() {
  const properties = allProperties.useLoaderData()
  return <ul>{properties.map(prop => <li><Property {...prop} /></li>)}</ul>
}

export { Properties as Component }
