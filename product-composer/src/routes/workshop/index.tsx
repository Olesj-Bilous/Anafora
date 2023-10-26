import { NavLink, Outlet } from 'react-router-dom';

function Workshop() {
  return (<>
    <p>Let's get to work!</p>
    <nav>
      <NavLink to='types'>Types</NavLink>
      <NavLink to='properties'>Properties</NavLink>
    </nav>
    <Outlet />
  </>
  );
}

export { Workshop as Component }
