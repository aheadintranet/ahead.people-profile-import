import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';

// https://astro.build/config
export default defineConfig({
	integrations: [
		starlight({
			title: 'Profile Import docs',
			logo: {
				light: '/src/assets/ahead--light.png',
				dark: '/src/assets/ahead--dark.png',
			},
			social: {
				linkedin: 'https://www.linkedin.com/company/aheadintranet/',
				github: 'https://github.com/aheadintranet/ahead.people-profile-import',
				email: 'mailto:support@aheadintranet.com',
			},
			sidebar: [
				{
					label: 'Guides',
					autogenerate: { directory: 'guides' },
				},
				{
					label: 'Reference',
					autogenerate: { directory: 'reference' },
				},
			],
		}),
	],
});
