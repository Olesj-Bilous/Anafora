import { defineContext } from "define-context";

export interface IAuthenticationContext {
  signedIn: boolean
}

export const [AuthenticationContext, useAuthenticationContext] = defineContext<IAuthenticationContext>("Authentication")
