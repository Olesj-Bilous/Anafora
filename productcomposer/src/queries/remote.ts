

export default async function remote(
  path: string,
  method: 'GET' | 'PUT' | 'POST' | 'DELETE' = 'GET',
  body?: any
): Promise<any> {
  const url = new URL('api' + path, 'https://localhost:7166')
  const headers = new Headers()
  if (method === 'PUT' || method === 'POST') headers.append('Content-Type', 'application/json')
  const response = await fetch(url, {
    method,
    headers,
    body: body && JSON.stringify(body)
  })
  return method === 'GET' ? response.json() : response.ok
}
