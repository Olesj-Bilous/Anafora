import { QueryClient, QueryFunction, QueryKey } from "@tanstack/react-query";
import { LoaderFunction, Params } from "react-router-dom";

export interface RouteFunctionArgs<P extends string = string> {
  request: Request
  params: Params<P>
}

export default function buildRouteQuery(queryClient: QueryClient) {
  return {
    deriveQueryKey<K extends QueryKey = QueryKey, P extends string = string>(deriver: (args: RouteFunctionArgs<P>) => K) {
      return {
        setQueryFn<T, Q = any>(queryFn: QueryFunction<T, K, Q>): {
          loader: LoaderFunction
        } {
          return {
            async loader(args) {
              const queryKey = deriver(args)
              return await queryClient.getQueryData(queryKey) ?? queryClient.fetchQuery({ queryKey, queryFn })
            }
          }
        }
      }
    }
  }
}
