import { NavLink, Outlet } from "react-router-dom"
import { allTypeQueries } from "../.."
import { useState } from "react"

function Properties() {
  const properties = allTypeQueries.useLoaderData()
  const [focus, setFocus] = useState(0)
  return <>
    <ul>
      {
        properties.map(({ id, name }, i) => <li key={i}>
          <NavLink to={`/workshop/properties/${id}`} onClick={() => setFocus(i)}>{name}</NavLink>
          {focus === i && <Outlet />}
        </li>)
      }
    </ul>
  </>
}

export { Properties as Component }
