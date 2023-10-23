import { ObjectSchema } from "yup"
import { remoteArray, remoteObject } from "../utils/castRemoteData"
import remote from "./remote"
import setQueryFn, { Mutor } from "../utils/setQueryFn"

export type AllKey = [model: string]

export function all<T extends object>(schema: ObjectSchema<T>, model: string) {
  return setQueryFn(
    async ({ queryKey: [model] }: { queryKey: AllKey }) => remoteArray(schema).cast((await remote(`/${model}/all`)))
  ).deriveQueryKey(
    () => [model]
  )
}

export type GetKey = [model: string, id: string]

export function get<T extends object>(schema: ObjectSchema<T>, model: string) {
  return setQueryFn(
    async ({ queryKey: [model, id] }: { queryKey: GetKey }) => remoteObject(schema).cast(await remote(`/${model}/get?id=${id}`))
  ).deriveQueryKey<'id'>(
    ({ id }) => [model, id ?? '']
  )
}

export type RelationKey = [model: string, relation: string, id: string]

export function relation<T extends object>(schema: ObjectSchema<T>, model: string, relation: string) {
  return setQueryFn(
    async ({ queryKey: [model, relation, id] }: { queryKey: RelationKey }) => remoteArray(schema).cast(await remote(`/${model}/${relation}?id=${id}`))
  ).deriveQueryKey<'id'>(
    ({ id }) => [model, relation, id ?? '']
  )
}

export type AddOrUpdateKey = [model: string, action: 'add' | 'update']

export function buildAddOrUpdate(mutor: Mutor) {
  const builder = mutor.deriveMutationFn(
    ([model,action]: AddOrUpdateKey) => (t) => remote(`/${model}/${action}`, action === 'add' ? 'POST' : 'PUT', t)
  )
  return <T>(model: string) => ({
    add: builder.setMutationKey<T>([model, 'add']),
    update: builder.setMutationKey<T>([model, 'update'])
  })
}

export type RemoveKey = [model: string, action: 'remove']

export function buildRemove(mutor: Mutor) {
  const builder = mutor.deriveMutationFn<RemoveKey, string>(
    ([model]) => id => remote(`/${model}/remove?id=${id}}`, 'DELETE')
  )
  return (model: string) => builder.setMutationKey([model, 'remove'])
}
