

export default function nameComponent<P>(Component: React.FC<P>): React.FC<P> {
  return Object.assign(Component, { displayName: Component.name })
}
