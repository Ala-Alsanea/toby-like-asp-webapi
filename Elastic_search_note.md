## Query Types

- match
    - field name
        - query: text
        - operator: and, or, etc.
        - minimum_should_match: number of search terms in query

- match_phrase
    - field name
        - query: text

- match_phrase_prefix
    - field name
        - query: text

- multi_match
    - query: text
    - fields: list of field names
    - type: best_fields, most_fields, cross_fields, phrase, phrase_prefix

- bool
    - must: list of queries
    - filter: list of queries
    - should: list of queries
    - must_not: list of queries

- range
    - field name
        - gte: greater than or equal to
        - lte: less than or equal to
        - gt: greater than
        - lt: less than

- term
    - field name
        - value: term

- terms
    - field name
        - values: list of terms

- exists
    - field name

- prefix
    - field name
        - value: prefix

- wildcard
    - field name
        - value: wildcard pattern

- regexp
    - field name
        - value: regular expression

- fuzzy
    - field name
        - value: text
        - fuzziness: auto, 0, 1, 2, etc.

- type
    - value: type name

- ids
    - values: list of ids

## Aggregation Types

- Bucket Aggregations
    - terms
        - field name
        - size
    - significant_terms
        - field name
        - size
    - range
        - field name
        - ranges: list of ranges
    - date_range
        - field name
        - ranges: list of date ranges
    - histogram
        - field name
        - interval
    - date_histogram
        - field name
        - interval
    - geo_distance
        - field name
        - origin: lat, lon
        - ranges: list of distance ranges
    - geohash_grid
        - field name
        - precision

- Metric Aggregations
    - avg
        - field name
    - sum
        - field name
    - min
        - field name
    - max
        - field name
    - stats
        - field name
    - extended_stats
        - field name
    - cardinality
        - field name
    - weighted_avg
        - value: field name
        - weight: field name
    - percentile_ranks
        - field name
        - values: list of values
    - percentiles
        - field name
        - percents: list of percentiles
    - top_hits
        - size
        - from
        - sort
    - geo_bounds
        - field name
    - geo_centroid
        - field name

- Matrix Aggregations
    - matrix_stats
        - fields: list of field names

- Pipeline Aggregations
    - avg_bucket
        - buckets_path
    - sum_bucket
        - buckets_path
    - min_bucket
        - buckets_path
    - max_bucket
        - buckets_path
    - stats_bucket
        - buckets_path
    - extended_stats_bucket
        - buckets_path
    - percentiles_bucket
        - buckets_path
    - moving_avg
        - buckets_path
    - cumulative_sum
        - buckets_path
    - bucket_script
        - buckets_path
        - script
    - bucket_selector
        - buckets_path
        - script
    - bucket_sort
        - sort
    - serial_diff
        - buckets_path
    - derivative
        - buckets_path
    - cumulative_cardinality
        - buckets_path

- Transform Aggregations
    - scripted_metric
        - init_script
        - map_script
        - combine_script
        - reduce_script