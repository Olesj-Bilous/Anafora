import { Form } from "../../components/editable/Form"
import { useEditor } from 'reactive-editable'
import { SignInModel, useSignIn } from "../../queries/builders"

function SignIn() {
  const { loading, error, signIn } = useSignIn()
  const { property, ...cfg } = useEditor<SignInModel>([{
    email: '',
    password: ''
  }, signIn])
  const [email, setEmail] = property('email')
  const [password, setPassword] = property('password')

  return <Form isValid={true} {...cfg}>
    <div className="form-status">
      {loading && 'loading'}
      {error && error.message}
    </div>
    <label htmlFor='email'>email</label>
    <input id='email' type='text' value={email} onChange={({ target: { value } }) => setEmail(value)} />
    <label htmlFor='password'>password</label>
    <input id='password' type='text' value={password} onChange={({ target: { value } }) => setPassword(value)} />
  </Form>
}

export { SignIn as Component }
