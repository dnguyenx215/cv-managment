import SendMailApi from '~/service/SendMailApi.ts';
import CvApi from '~/service/cv.api.service';
import ColumnRelationshipApi from '~/service/ColumnRelationshipApi.ts';
import ColumnLayoutApi from '~/service/ColumnLayoutApi.ts';
import https from '~/service/Base/axiousInstance'
let apiFactory;
// This plugin provides an object `apiFactory` which contains all the api classes (e.g. `ExampleApi`)
// that can be used to make API requests to the server. This plugin is used to create these classes
// and make them available to the rest of the application.

// The function passed to `defineNuxtPlugin` is called by Nuxt when it creates the Nuxt context
// (i.e. when the Nuxt app is being started or reloaded). This function has access to the Nuxt
// context, which includes the `$axios` property, which is an instance of Axios that has
// been configured to work with Nuxt. We use this instance to create instances of our API classes.

// We create an Factory` with properties for each of our API classes, and assign it
// to the exported `apiFactory` variable. We also export the `apiFactory` variable so that it can
// be imported and used by other parts of the applobject `apiication.

// The name `apiFactory` was chosen because it makes it clear what this plugin does, and
// it's easy to understand how to use it. The name could be improved, but it's a start!

export default function () {

    apiFactory = {
        sendMail: new SendMailApi(),
        cv: new CvApi(),
        columnLayout: new ColumnLayoutApi(),
        columnRelationship: new ColumnRelationshipApi()

    };
}

export { apiFactory };
