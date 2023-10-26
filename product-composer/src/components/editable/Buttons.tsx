import { icon } from '@fortawesome/fontawesome-svg-core/import.macro'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { Save as SaveBase, Revert as RevertBase } from 'reactive-editable'

const save = icon({ name: "check" }),
  revert = icon({ name: "undo" })

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
