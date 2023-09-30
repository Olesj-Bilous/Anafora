import { QueryClient } from "@tanstack/react-query";
import { LoaderFunction } from "react-router-dom";
import fetchRemoteData from "../..";


export function typesQuery(client: QueryClient): LoaderFunction {
  const queryKey = ['types']
  return async ({ request, params }) => client.getQueryData(queryKey) ?? client.fetchQuery({
    queryKey, queryFn: async () => await fetchRemoteData(request, {})
  })
}
