<template>
    <div class="p-6 max-w-lg mx-auto">
        <h1 class="text-2xl font-semibold mb-4">Create Booking</h1>

        <div class="mb-4" :hidden="initialized">
            <UButton :disabled="initialized" :loading="loading" @click="initializeBooking">Start booking
            </UButton>
        </div>
        <UCard class="p-4" :hidden="!initialized">
            <!-- Booking Form -->
            <UForm :state="handout">
                <div class="mb-4">
                    <UInput v-model="booking.bookingNumber" label="Bookingnumber" disabled />
                </div>

                <div class="mb-4 flex gap-4">
                    <!-- Registration Number Input -->
                    <UInput v-model="booking.registrationNumber" label="Registration Number" placeholder="Enter reg nr"
                        class="flex-1" :disabled="!initialized" />

                    <!-- Search/Create Button -->
                    <UButton :loading="searching" @click="handleSearch" v-if="!carNotFound">
                        Search
                    </UButton>
                    <UButton :loading="creating" @click="handleCreate" v-if="carNotFound">
                        Create
                    </UButton>
                </div>

                <div class="mb-4 flex gap-4">
                    <!-- Car Type Dropdown -->
                    <USelect v-model="booking.type" :options="items" :disabled="!initialized" />
                </div>

                <div class="mb-4">
                    <UInput placeholder="Odometer" v-model="booking.odometer" label="Odometer"
                        :disabled="!initialized || !carNotFound">
                        <template #trailing>
                            <span class="text-gray-500 dark:text-gray-400 text-xs">KM</span>
                        </template>
                    </UInput>
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.customerIdentification" label="Customer ID" placeholder="customer id"
                        :disabled="!initialized" />
                </div>

                <div class="mb-4">
                    <UInput v-model="booking.date" label="Date" type="datetime-local" :disabled="!initialized" />
                </div>

            </UForm>

            <div class="mb-4">
                <UButton :loading="loading" @click="createBooking">Create Booking</UButton>
            </div>
        </UCard>
    </div>
</template>

<script setup>
import { ref, computed } from "vue";

const initialized = ref(false);
const items = ["SmallCar", "StationWagon", "Truck"];
const config = useRuntimeConfig();
const router = useRouter();
const handout = ref({});

const booking = ref({
    bookingNumber: "",
    registrationNumber: "",
    customerIdentification: "",
    type: "",
    date: "",
    odometer: "",
    carId: 0,
});

const loading = ref(false);

const createBooking = async () => {
    loading.value = true;
    try {
        const response = await fetch(`${config.public.apiBaseUrl}/bookings/${booking.value.bookingNumber}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(booking.value)
        });

        router.push("/handout");
    } catch (error) {
        console.error("Error creating booking:", error);
    } finally {
        loading.value = false;
    }
}

const initializeBooking = async () => {
    loading.value = true;

    try {
        const response = await fetch(`${config.public.apiBaseUrl}/bookings/init`, { method: "POST" });
        const data = await response.json();
        console.log("Data:", data);
        // Populate form fields
        booking.value = {
            bookingNumber: data.bookingNumber,
        };
        console.log("Booking initialized:", booking.value);
        initialized.value = true;
    } catch (error) {
        console.error("Error initializing booking:", error);
    } finally {
        loading.value = false;
    }
};


const searching = ref(false);
const carNotFound = ref(false);
const creating = ref(false);

const handleCreate = async () => {
    if (!booking.value.registrationNumber ||
        !booking.value.odometer ||
        !booking.value.type
    ) return;

    try {
        creating.value = true;
        const response = await fetch(`${config.public.apiBaseUrl}/cars/`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                'registrationNumber': booking.value.registrationNumber,
                'odometer': booking.value.odometer,
                'type': booking.value.type
            })
        });
        
        const data = response.json();
        booking.value.carId = data.id;

        await handleSearch();
    } catch (error) {
        console.error("Error fetching car:", error);
    } finally {
        creating.value = false;
    }
    
}


const handleSearch = async () => {
    if (!booking.value.registrationNumber) return; // Prevent empty search
    searching.value = true;

    try {
        const response = await fetch(`${config.public.apiBaseUrl}/cars/${booking.value.registrationNumber}`, { method: "GET" });

        if (response.status === 404) {
            carNotFound.value = true; // Show "Create" button
            return;
        }

        const car = await response.json();

        const carTypeMap = {
            1: "SmallCar",
            2: "StationWagon",
            3: "Truck"
        };

        booking.value.type = carTypeMap[car.type] || "";
        booking.value.odometer = car.odemeter;
        booking.value.carId = car.id
        carNotFound.value = false; // Keep button as "Search"
    } catch (error) {
        console.error("Error fetching car:", error);
    } finally {
        searching.value = false;
    }
};

</script>
