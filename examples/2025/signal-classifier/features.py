# Extract classification features from a short IQ capture (NumPy array of complex samples).
# Returns a flat feature vector suitable for sklearn classifiers.
import numpy as np
from numpy.fft import fft, fftshift

def spectral_features(iq: np.ndarray, n_fft: int = 1024) -> np.ndarray:
    """Compute spectral flatness, centroid, bandwidth, and power from IQ samples."""
    spectrum = np.abs(fftshift(fft(iq, n=n_fft))) ** 2
    freqs = np.linspace(-0.5, 0.5, n_fft)

    # Normalize to form a distribution
    psd = spectrum / spectrum.sum()

    # Spectral centroid
    centroid = float(np.sum(freqs * psd))

    # Spectral flatness (geometric mean / arithmetic mean of power spectrum)
    log_mean = np.mean(np.log(spectrum + 1e-10))
    arith_mean = np.mean(spectrum)
    flatness = float(np.exp(log_mean) / (arith_mean + 1e-10))

    # Occupied bandwidth (90% power bandwidth)
    cumulative = np.cumsum(psd)
    lo = int(np.searchsorted(cumulative, 0.05))
    hi = int(np.searchsorted(cumulative, 0.95))
    bandwidth = float(freqs[hi] - freqs[lo])

    # Total power (dB relative to full scale)
    power_db = float(10 * np.log10(np.mean(np.abs(iq) ** 2) + 1e-10))

    return np.array([centroid, flatness, bandwidth, power_db], dtype=np.float32)
