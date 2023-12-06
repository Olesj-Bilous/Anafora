import { Outlet } from 'react-router-dom';
import './root.css';
import { NavLink } from 'react-router-dom';

function Root() {
  return (
    <>
      <nav>
        <NavLink to={'products'}>Products</NavLink>
        <NavLink to={'signin'}>Sign in</NavLink>
      </nav>
      <main>
        <Outlet />
      </main>
    </>
  );
}

export default Root;
