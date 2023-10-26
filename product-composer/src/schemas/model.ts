import { ObjectSchema, object, string } from "yup";

export const model = {
  id: string().required().uuid()
}

export const namedModel = {
  ...model,
  name: string().required()
}

export const typeSchema: ObjectSchema<Type> = object(namedModel)

export const propertySchema: ObjectSchema<Property> = object(namedModel)

export const valueSchema: ObjectSchema<Value> = object({
  ...model,
  value: string().required()
})
