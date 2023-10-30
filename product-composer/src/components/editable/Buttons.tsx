import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { Save as SaveBase, Revert as RevertBase } from 'reactive-editable'
import { save, revert } from './icons'

export function Save() {
  return <SaveBase>
    <FontAwesomeIcon icon={save} />
  </SaveBase>
}

export function Revert() {
  return <RevertBase>
    <FontAwesomeIcon icon={revert} />
  </RevertBase>
}
