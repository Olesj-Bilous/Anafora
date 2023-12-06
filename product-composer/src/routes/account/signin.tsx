import { Form } from "../../components/editable/Form"
import { useEditor } from 'reactive-editable'
import { SignInModel, useSignInAlt } from "../../queries/builders"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { revert, save } from "../../components/editable/icons"

function SignIn() {
  const { loading, error, Fetcher } = useSignInAlt()

  return <Fetcher method="post">
    <div className="form-status">
      {loading && 'loading'}
      {error && error.message}
    </div>
    <label htmlFor='email'>email</label>
    <input id='email' type='email' name="email" />
    <label htmlFor='password'>password</label>
    <input id='password' type='password' name="password" />
    <button type="submit">
      <FontAwesomeIcon icon={save} />
    </button>
    <button type="reset">
      <FontAwesomeIcon icon={revert} />
    </button>
  </Fetcher>
}

export { SignIn as Component }
