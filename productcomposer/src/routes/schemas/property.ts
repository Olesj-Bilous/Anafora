import { object, string } from "yup";

export const model = {
  id: string().required().uuid()
}

export const namedModel = {
  ...model,
  name: string().required()
}

export const propertySchema = object(namedModel)

export const typesSchema = object(namedModel)

export const valuesSchema = object({
  ...model,
  value: string().required()
})
