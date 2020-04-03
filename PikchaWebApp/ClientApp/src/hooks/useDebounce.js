import { useState, useEffect } from 'react';

/**
 * Takes an arbitrary value and returns the value only if the specified
 * delay threshold is exceeded.
 */
export const useDebounce = (value, delay) => {
  // Holds and sets debounced state.
  const [debouncedValue, setDebouncedValue] = useState(value);

  useEffect(
    () => {
      // Sets `debouncedValue` to the passed-in value after the specified delay.
      const handler = setTimeout(() => {
        setDebouncedValue(value);
      }, delay);

      return () => {
        clearTimeout(handler);
      };
    },

    [value],
  );

  return debouncedValue;
};
