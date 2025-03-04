<template>
    <div class="p-6 max-w-lg mx-auto">
        <h1 class="text-2xl font-semibold mb-4">Dealership Setup</h1>

        <UCard class="p-4">
            <!-- Dealership Setup Form -->
            <UForm :state="formState">
                <div class="mb-4">
                    <UInput label="Dealership Name" placeholder="Dealership Name" v-model="dealership.dealerShipName" />
                </div>

                <div class="mb-4">
                    <UInput label="Shortname" placeholder="Shortname" v-model="dealership.dealerShipShortName" />
                </div>

                <div class="mb-4 flex gap-4">
                    <UInput label="Base Day Fee" placeholder="Base Day Fee" v-model="dealership.baseDayFee" />
                    <UInput label="Base Distance Fee" placeholder="Base Distance Fee"
                        v-model="dealership.baseDistanceFee" />
                </div>
            </UForm>

            <!-- Save Button -->
            <div class="mt-4">
                <UButton :loading="loading" @click="saveDealershipSettings">
                    Save
                </UButton>
            </div>
        </UCard>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'

// Tracks whether a request is in progress
const loading = ref(false)

// If you need reactive form state for <UForm>:
const formState = ref({})

// Data model for the dealership details
const dealership = ref({
    dealerShipName: '',
    dealerShipShortName: '',
    baseDayFee: '',
    baseDistanceFee: ''
})


const getDealerShipSettings = async () => {
    loading.value = true
    try {
        // Example endpoint; change to the actual URL you need
        const response = await fetch('http://localhost:5270/settings')
        const data = await response.json()
        dealership.value = data
    } catch (error) {
        console.error('Error fetching dealership settings:', error)
    } finally {
        loading.value = false
    }
}

onMounted(getDealerShipSettings)


const saveDealershipSettings = async () => {
    loading.value = true
    try {
        // Example endpoint; change to the actual URL you need
        const response = await fetch('http://localhost:5270/settings', {
            method: 'POST', // or 'PUT', depending on your API
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dealership.value)
        })
        // Handle response as needed
    } catch (error) {
        console.error('Error saving dealership:', error)
    } finally {
        loading.value = false
    }
}
</script>