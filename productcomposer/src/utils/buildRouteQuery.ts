import { QueryClient, QueryFunction, QueryKey } from "@tanstack/react-query";
import { LoaderFunction, Params } from "react-router-dom";

export interface RouteFunctionArgs<P extends string = string> {
  request: Request
  params: Params<P>
}

export default function buildRouteQuery(queryClient: QueryClient) {
  return {
    setQueryFn<K extends QueryKey = QueryKey, T = unknown, P = any>(queryFn: QueryFunction<T, K, P>) {
      return {
        deriveQueryKey<Q extends string = string>(deriver: (args: RouteFunctionArgs<Q>) => K): {
          loader: LoaderFunction
        } {
          return {
            async loader(args) {
              const queryKey = deriver(args)
              return queryClient.getQueryData(queryKey) ?? await queryClient.fetchQuery({ queryKey, queryFn })
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
