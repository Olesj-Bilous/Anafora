import { mutor } from "..";
import { buildAddOrUpdate } from "./builders";

const addOrUpdate = buildAddOrUpdate(mutor)

export const { add: addValue, update: updateValue } = addOrUpdate<Value>('value')
export const { add: addProperty, update: updateProperty } = addOrUpdate<Property>('property')
