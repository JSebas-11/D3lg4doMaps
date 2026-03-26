import Link from '@docusaurus/Link';

export default function HomepageComplements() {
  return (
    <section style={{ padding: '4rem 0', textAlign: 'center' }}>
        <div className="container">
          <h2>Why D3lg4doMaps?</h2>
          <p style={{ maxWidth: 700, margin: '1rem auto' }}>
            Working directly with Google Maps APIs can be verbose and fragmented.
            D3lg4doMaps centralizes configuration in a Core module and provides clean,
            high-level abstractions for features like Places and Routing — reducing complexity
            while keeping full control.
          </p>

          <div style={{ marginTop: '2rem' }}>
            <Link className="button button--primary button--lg" href="/docs/intro">
              Get Started
            </Link>
            <a className="button button--secondary button--lg"
              href="https://github.com/JSebas-11/D3lg4doMaps"
              target='_blank'
              style={{ marginLeft: '1rem' }}>
              View on GitHub
            </a>
          </div>
        </div>
      </section>
  );
}