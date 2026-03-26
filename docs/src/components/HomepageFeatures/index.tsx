import type {ReactNode} from 'react';
import clsx from 'clsx';
import Heading from '@theme/Heading';
import styles from './styles.module.css';

type FeatureItem = {
  title: string;
  Svg: React.ComponentType<React.ComponentProps<'svg'>>;
  description: ReactNode;
};

const FeatureList: FeatureItem[] = [
  {
    title: 'Simple Integration',
    Svg: require('@site/static/img/easyIntegration.svg').default,
    description: (
      <>
        Install via NuGet and configure the Core once (API key, language, etc) and all 
        modules like Places and Routing automatically inherit that configuration.
        Start making requests in seconds, no boilerplate, no friction.
      </>
    )
  },
  {
    title: 'Built for .NET Developers',
    Svg: require('@site/static/img/developerDriven.svg').default,
    description: (
      <>
        Designed with dependency injection, immutability, and strong typing in mind —
        following modern .NET best practices.
      </>
    )
  },
  {
    title: 'High-Level API',
    Svg: require('@site/static/img/highLevel.svg').default,
    description: (
      <>
        Clean, expressive methods that abstract away HTTP complexity, letting you focus
        on building features instead of handling raw API calls.
      </>
    )
  },
  {
    title: 'Modular, Core-Driven Architecture',
    Svg: require('@site/static/img/coreDriven.svg').default,
    description: (
      <>
        Built around a central Core module that handles configuration and shared infrastructure.
        Feature modules like Places and Routing extend Core, ensuring consistency and simplicity
        across the entire SDK.
      </>
    ) 
  },
  {
    title: 'Fluent Builders',
    Svg: require('@site/static/img/fluentApi.svg').default,
    description: (
      <>
        Construct requests using a fluent API that prevents invalid states and improves
        readability.
      </>
    ),
  },
  {
    title: 'Clean Error Handling',
    Svg: require('@site/static/img/errorHandling.svg').default,
    description: (
      <>
        Consistent exception model that wraps API errors into predictable and meaningful
        .NET exceptions.
      </>
    ),
  }
];

function Feature({title, Svg, description}: FeatureItem) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} role="img" />
      </div>
      <div className="text--center padding-horiz--md">
        <Heading as="h3">{title}</Heading>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures(): ReactNode {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}