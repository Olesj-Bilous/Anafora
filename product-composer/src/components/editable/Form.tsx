import { ReactNode } from 'react';
import { Editable, EditPermissionContext } from "reactive-editable";
import { Save, Revert } from './Buttons';

export interface FormConfig {
  isTouched: boolean
  isValid: boolean
  save(): void
  revert(): void
}

export function Form({ children, ...cgf }: { children?: ReactNode } & FormConfig) {
  return <EditPermissionContext.Provider value={true}><Editable {...cgf}>
    <form>
      <div className='form-body'>
        {children}
      </div>
      <div className="form-control">
        <Save />
        <Revert />
      </div>
    </form>
  </Editable></EditPermissionContext.Provider> 
}
