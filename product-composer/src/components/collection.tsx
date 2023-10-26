import { FC } from "react";


export function collection<T, K extends string = 'element'>(name: K, plural: string = name + 's', Element: FC<{[Key in K]: T}>) {
  return function Collection({elements}: { elements: T[] }) {
    
  }
}
