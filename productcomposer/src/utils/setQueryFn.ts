import { MutationFunction, MutationKey, QueryClient, QueryFunction, QueryKey, UseMutationResult, UseQueryResult, useMutation, useQuery } from "@tanstack/react-query";
import { Params, useLoaderData } from "react-router-dom";

export interface LoaderKeyArgs<P extends string = string> {
  params: Params<P>
}

export interface QueryControl<T = unknown, Pm extends string = string> {
  loader: (client: QueryClient) => (args: LoaderKeyArgs<Pm>) => Promise<T>
  useLoaderData: () => T
  useQuery: (params: Params<Pm>) => UseQueryResult<T>
}

export default function setQueryFn<K extends QueryKey = QueryKey, T = unknown, Pg = any>(
  queryFn: QueryFunction<T, K, Pg>
) {
  return {
    deriveQueryKey<Pm extends string = string>(deriver: (params: Params<Pm>) => K): QueryControl<T, Pm> {
      return {
        loader: client => async ({ params }) => {
          const queryKey = deriver(params)
          return client.getQueryData(queryKey) ?? await client.fetchQuery({ queryKey, queryFn })
        },
        useLoaderData: useLoaderData as () => T,
        useQuery(params) {
          const queryKey = deriver(params)
          return useQuery({ queryKey, queryFn })
        }
      }
    }
  }
}

export interface Mutor {
  deriveMutationFn: <M extends MutationKey, T = any>(
    deriver: <S extends T = T>(m: M) => MutationFunction<Response, S>
  ) => {
    setMutationKey: <Tx extends T = T>(key: M) => {
      useMutation: () => UseMutationResult<Response, unknown, Tx>
    }
  }
}

export const createMutor = (client: QueryClient): Mutor => ({
  deriveMutationFn<M extends MutationKey, T = any>(
    deriver: <S extends T = T>(m: M) => MutationFunction<Response, S>
  ) {
    return {
      setMutationKey<Tx extends T = T>(key: M) {
        client.setMutationDefaults(key, { mutationFn: deriver(key) })
        return {
          useMutation: () => useMutation<Response, unknown, Tx>({ mutationKey: key })
        }
      }
    }
  }
})
