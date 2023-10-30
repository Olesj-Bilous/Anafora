import { useState, useCallback, useEffect } from "react"
import { ObjectSchema } from "yup"
import { remoteArray, remoteObject } from "../utils/castRemoteData"
import remote from "./remote"
import { setQueryFn, Mutor } from "../utils/setQueryFn"
import { redirect, useFetcher } from "react-router-dom"
import { Settable } from "reactive-editable"

export type AllKey = [model: string]

export function all<T extends object>(schema: ObjectSchema<T>, model: string) {
  return setQueryFn(
    async ({ queryKey: [modelName] }: { queryKey: AllKey }) => remoteArray(schema).cast(await (await remote(`/${modelName}/all`)).json())
  ).deriveQueryKey(
    () => [model]
  )
}

export type GetKey = [model: string, id: string]

export function get<T extends object>(schema: ObjectSchema<T>, model: string) {
  return setQueryFn(
    async ({ queryKey: [modelName, id] }: { queryKey: GetKey }) => remoteObject(schema).cast(await (await remote(`/${modelName}/get?id=${id}`)).json())
  ).deriveQueryKey<'id'>(
    ({ id }) => [model, id ?? '']
  )
}

export type RelationKey = [model: string, relation: string, id: string]

export function relation<T extends object>(schema: ObjectSchema<T>, model: string, relation: string) {
  return setQueryFn(
    async ({ queryKey: [model, relation, id] }: { queryKey: RelationKey }) => remoteArray(schema).cast(await (await remote(`/${model}/${relation}?id=${id}`)).json())
  ).deriveQueryKey<'id'>(
    ({ id }) => [model, relation, id ?? '']
  )
}

export interface SignInModel {
  email: string
  password: string
}

export function useSignIn() {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<Error>()
  return {
    signIn: useCallback(async (model: SignInModel) => {
      setLoading(true)
      try {
        const singinIn = await signIn(model)
        if (!singinIn.ok) throw new Error('Signing in not ok.')
        // redirect('/')
      } catch (error) {
        setLoading(false)
        if (error instanceof Error)
          setError(error)
        else
          setError(new Error('Unknown error occurred while signing in.'))
      }
    }, []),
    loading, error
  }
}

export function useSignInAlt() {
  const { state, data, Form } = useFetcher<Response>()
  const [error, setError] = useState<Error>()
  useEffect(() => {
    if (!data) return
    if (data?.ok) 
      redirect('/')
    else
      setError(new Error(data?.statusText))
  }, [data])
  return {
    loading: state === 'idle',
    error,
    Fetcher: Form
  }
}

export function signIn(model: SignInModel) {
  return remote('/account/signin', 'POST', model)
}

export type AddOrUpdateKey = [model: string, action: 'add' | 'update']
export type AddKey = [model: string, action: 'add']
export type UpdateKey = [model: string, action: 'update']

export interface Model {
  id: string
}

export function buildAddOrUpdate(mutor: Mutor) {
  function deriveMutationFn([modelName, action]: AddOrUpdateKey) {
    return <T extends Model>(model: T) => remote(`/${modelName}/${action}`, action === 'add' ? 'POST' : 'PUT', model)
  }
  const builder = {
    add: mutor.deriveMutation<AddKey, Model>(
      ([modelName, action]) => ({
        mutationFn: deriveMutationFn([modelName, action]),
        queryKeys: (model) => ({
          all: [modelName]
        })
      })
    ),
    update: mutor.deriveMutation<UpdateKey, Model>(
      ([modelName, action]) => ({
        mutationFn: deriveMutationFn([modelName, action]),
        queryKeys: (model) => ({
          get: [modelName, model.id]
        })
      })
    )
  }
  return <T extends Model>(model: string) => ({
    add: builder.add.setMutationKey<T>([model, 'add']),
    update: builder.update.setMutationKey<T>([model, 'update'])
  })
}

export type RemoveKey = [model: string, action: 'remove']

export function remove(mutor: Mutor) {
  const builder = mutor.deriveMutation<RemoveKey, string>(
    ([model]) => ({
      mutationFn: id => remote(`/${model}/remove?id=${id}}`, 'DELETE')
    })
  )
  return (model: string) => builder.setMutationKey([model, 'remove'])
}

export type UpdateRelationKey = [model: string, relation: string, action: 'add' | 'remove']

export type UpdateRelationArgs = [id: string, relationId: string]

export function buildUpdateRelation(mutor: Mutor) {
  function deriveMutationFn([model, relation, action]: UpdateRelationKey) {
    return ([id, relationId]: UpdateRelationArgs) => remote(
      `/${model}/${action}${relation}?id=${id}&relationid=${relationId}`,
      action === 'add' ? 'POST' : 'DELETE'
    )
  }
  const builder = mutor.deriveMutation<UpdateRelationKey, UpdateRelationArgs>(
    ([model, relation, action]) => ({
      mutationFn: deriveMutationFn([model, relation, action]),
      queryKeys: ([id, relationId]) => ({
        relation: [model, relation, id]
      })
    })
  )
  return (model: string, relation: string) => ({
    add: builder.setMutationKey([model, relation, 'add']),
    remove: builder.setMutationKey([model, relation, 'remove'])
  })
}
