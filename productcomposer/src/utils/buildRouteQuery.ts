import { QueryClient, QueryFunction, QueryKey, UseQueryResult, useQuery } from "@tanstack/react-query";
import { Params, useLoaderData } from "react-router-dom";

export interface LoaderKeyArgs<P extends string = string> {
  params: Params<P>
}

export interface QueryControl<T = unknown, P extends string = string> {
  loader: (args: LoaderKeyArgs<P>) => Promise<T>
  useLoaderData: () => T
  useQuery: (args: Params<P>) => UseQueryResult<T>
}

export default function buildRouteQuery(queryClient: QueryClient) {
  return {
    setQueryFn<K extends QueryKey = QueryKey, T = unknown, P = any>(queryFn: QueryFunction<T, K, P>) {
      return {
        deriveQueryKey<Q extends string = string>(deriver: (params: Params<Q>) => K): QueryControl<T, Q> {
          return {
            async loader({params}) {
              const queryKey = deriver(params)
              return queryClient.getQueryData(queryKey) ?? await queryClient.fetchQuery({ queryKey, queryFn })
            },
            useLoaderData: useLoaderData as () => T,
            useQuery(args) {
              const queryKey = deriver(args)
              return useQuery({queryKey, queryFn})
            }
          }
        }
      }
    }
  }
}

/*export function composeLoaders(loaders: Record<string, LoaderFunction>): LoaderFunction {
  return (args) => {
    const response = {}
    for (const key in loaders) {
      response[key] = 
    }
  }
}*/
