import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

// This runs in Node.js - Don't use client-side code here (browser APIs, JSX...)

const config: Config = {
  title: 'D3lg4doMaps',
  tagline: 'A clean .NET SDK for Google Maps APIs',
  favicon: 'img/D3lg4doMapsMinimalistLogo.ico',

  // Future flags, see https://docusaurus.io/docs/api/docusaurus-config#future
  future: {
    v4: true, // Improve compatibility with the upcoming Docusaurus v4
  },

  // Set the production url of your site here
  url: 'https://JSebas-11.github.io',
  baseUrl: '/D3lg4doMaps/',

  // GitHub pages deployment config.
  // If you aren't using GitHub pages, you don't need these.
  organizationName: 'JSebas-11', // Usually your GitHub org/user name.
  projectName: 'D3lg4doMaps', // Usually your repo name.

  onBrokenLinks: 'throw',

  // Even if you don't use internationalization, you can use this field to set
  // useful metadata like html lang. For example, if your site is Chinese, you
  // may want to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          editUrl: 'https://github.com/JSebas-11/D3lg4doMaps/tree/main/',
        },
        theme: { customCss: './src/css/custom.css' },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    // Replace with your project's social card
    image: 'img/docusaurus-social-card.jpg',
    colorMode: { respectPrefersColorScheme: true },
    navbar: {
      title: 'D3lg4doMaps',
      logo: {
        alt: 'D3lg4doMaps Logo',
        src: 'img/D3lg4doMapsLogo.png',
      },
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'tutorialSidebar',
          position: 'left',
          label: 'Docs',
        },
        {
          href: 'https://github.com/JSebas-11/D3lg4doMaps',
          label: 'GitHub',
          position: 'right',
        },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Packages',
          items: [
            { label: 'Core', to: '/docs/core/overview' },
            { label: 'Places', to: '/docs/places/overview' },
            { label: 'Routing', to: '/docs/routing/overview' }
          ],
        },
        {
          title: 'SDK Links',
          items: [
            { label: 'Github', href: 'https://github.com/JSebas-11/D3lg4doMaps' },
            { label: 'Nuget', href: 'https://www.nuget.org/packages?q=d3lg4doMaps' }
          ],
        },
        {
          title: 'Personal Info',
          items: [
            { label: 'Github', href: 'https://github.com/JSebas-11' },
            { label: 'Linkedin', href: 'https://www.linkedin.com/in/jsebas11/' },
            { label: 'Instagram', href: 'https://www.instagram.com/juans3_11.py/' }
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} D3lg4doMaps (JSebas-11). Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['csharp'],
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
