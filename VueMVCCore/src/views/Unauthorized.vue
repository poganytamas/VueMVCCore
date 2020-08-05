<template>
    <v-container fluid>
        <v-layout row
                  wrap>
            <v-flex xs12
                    text-xs-center
                    mt-5>
                <h1>Unauthorized</h1>
            </v-flex>
            <v-flex xs12
                    text-xs-center
                    mt-3>
                Your account is not authorized to access the page you requested.
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
  export default {
    name: 'Unauthorized'
  }
</script>
