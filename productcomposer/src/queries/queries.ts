import { propertySchema, typeSchema, valueSchema } from '../schemas/model';
import { all, relation } from './builders';

export const allTypes = all(typeSchema, 'type')
export const allProperties = all(propertySchema, 'property')

export const typeProperties = relation(propertySchema, 'type', 'properties')
export const propertyValues = relation(valueSchema, 'property', 'values')
