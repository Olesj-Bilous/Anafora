import { ObjectSchema, array } from "yup"

export function remoteArray<T extends object>(schema: ObjectSchema<T>): { cast: (value: any) => T[] } {
  return { cast: value => array(schema).default([]).json().cast(value) as T[] }
}

export function remoteObject<T extends object>(schema: ObjectSchema<T>): { cast: (value: any) => T | null } {
  return { cast: value => schema.nullable().default(null).json().cast(value) as T | null }
}
