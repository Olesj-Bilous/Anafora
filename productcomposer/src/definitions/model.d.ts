

interface Model {
  id: string
}

interface Type extends Model {
  name: string
}

interface Property extends Model {
  name: string
}

interface Value extends Model {
  value: string
}
