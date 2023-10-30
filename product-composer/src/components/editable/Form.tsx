import { ReactNode } from 'react';
import { Editable, EditConfig, EditPermissionContext } from "reactive-editable";
import { Revert, Save } from './Buttons';

export interface FormConfig extends Omit<EditConfig, 'mode' | 'editToggled' | 'remove'> {
}

export function Form({ children, ...cfg }: { children?: ReactNode } & FormConfig) {
  return <EditPermissionContext.Provider value={true}>
    <Editable {...cfg}>
      <div className='form-body'>
        {children}
      </div>
      <div className="form-control">
        <Save />
        <Revert />
      </div>
    </Editable>
  </EditPermissionContext.Provider>
}
