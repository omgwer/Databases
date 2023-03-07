export interface SearchParameters {
  startPoint?: string | null,
  finishPoint?: string | null,
  minRange?: string | null,
  maxRange?: string | null,
  busStopName?: string | null,
  placement?: string | null,
  isHavePavilion?: string | null,
  offset: number,
  order?: string | null,
  direction?: string|null
}
