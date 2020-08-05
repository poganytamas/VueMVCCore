<template>
    <v-app dark>
        <v-app-bar app>
            <v-toolbar-items>
                <v-btn exact
                       text
                       :to="{name: 'home'}">
                    Home
                </v-btn>
                <v-btn text
                       :to="{name: 'about'}">
                    About
                </v-btn>
            </v-toolbar-items>
        </v-app-bar>
        <v-main>
            <v-container fluid
                         grid-list-md>
                <router-view />
            </v-container>
        </v-main>
    </v-app>
</template>

<style>
    * {
        text-transform: none !important;
    }
</style>
